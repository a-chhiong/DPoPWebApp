import { BehaviorSubject, timer, takeUntil } from 'rxjs'; // Fix: import takeUntil from main
import { map, tap, takeWhile } from 'rxjs';
import { BaseViewModel } from './BaseViewModel.js';
import { apiManager } from '../managers/ApiManager.js';
import { tokenManager } from '../managers/TokenManager.js';
import { themeManager } from '../managers/ThemeManager.js';
import { Theme } from '../constants/Theme.js';

export class HomeViewModel extends BaseViewModel {
    constructor(host) {
        super(host);
        
        // 1. Private Subjects (Sources)
        this._user$ = new BehaviorSubject(null);
        this._remainingTime$ = new BehaviorSubject(-1);
        this._loading$ = new BehaviorSubject(false);

        // 2. Bound UI State (The "Hubs" are now internal)
        this.user = this.bind(this._user$);
        this.time = this.bind(this._remainingTime$, -1);
        this.theme = this.bind(themeManager.theme$, themeManager.current);
        this.loading = this.bind(this._loading$, false);

        this._timerSub = null;
    }

    async onConnect() {
        // Triggered automatically by Lit's hostConnected
        await this._initDashboard();
    }

    toggleTheme() {
        const current = themeManager.current;
        themeManager.setTheme(current === Theme.DARK ? Theme.LIGHT : Theme.DARK);
    }

    async _initDashboard() {
        this._startHeartbeat();
        
        if (this._user$.value) return;

        this._loading$.next(true);
        try {
            const res = await apiManager.authApi.get('/user');
            this._user$.next(res.data);
            this._startHeartbeat();
        } catch (err) {
            console.error("HomeViewModel: Profile Sync Failed", err);
        } finally {
            this._loading$.next(false);
        }
    }

    _startHeartbeat() {
        this._stopHeartbeat();
        const token = tokenManager.getAccessToken();
        const expiry = this._getExpiry(token);

        if (expiry <= 0) return;

        // Use takeUntil(this.destroy$) as a secondary safety net
        this._timerSub = timer(0, 1000).pipe(
            map(() => Math.max(0, expiry - Math.floor(Date.now() / 1000))),
            tap(timeLeft => {
                this._remainingTime$.next(timeLeft);
                if (timeLeft === 0) console.warn("Session Expired");
            }),
            takeWhile(timeLeft => timeLeft >= 0, true),
            takeUntil(this.destroy$) 
        ).subscribe();
    }

    _getExpiry(token) {
        if (!token) return -1;
        try {
            return JSON.parse(atob(token.split('.')[1])).exp;
        } catch (e) { return -1; }
    }

    _stopHeartbeat() {
        if (this._timerSub) {
            this._timerSub.unsubscribe();
            this._timerSub = null;
        }
    }

    async logout() {
        this._stopHeartbeat();
        try {
            const rt = tokenManager.getRefreshToken();
            if (rt) await apiManager.tokenApi.post("/logout", { refreshToken: rt });
        } finally {
            await tokenManager.clearTokens();
            this._user$.next(null);
            this._remainingTime$.next(-1);
        }
    }
}