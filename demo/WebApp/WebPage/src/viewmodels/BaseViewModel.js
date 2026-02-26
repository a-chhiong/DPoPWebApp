import { Subject, takeUntil } from 'rxjs';

/**
 * @class BaseViewModel
 * @description A Lifecycle-aware Lit Controller that manages RxJS state.
 */
export class BaseViewModel {
    constructor(host) {
        if (!host) {
            throw new Error(`[${this.constructor.name}] must be initialized with a 'host' (this)`);
        }

        this.host = host;
        this.destroy$ = new Subject();
        
        // Register the ViewModel as a Reactive Controller with Lit
        host.addController(this);
    }

    // --- Lit Controller Interface ---
    
    hostConnected() {
        this.onConnect();
    }

    hostDisconnected() {
        this.onDisconnect();
        // Automatic cleanup of all RxJS streams tied to this ViewModel
        this.destroy$.next();
        this.destroy$.complete();
    }

    // --- Android-Style Lifecycle Hooks ---

    onConnect() {
        console.log(`[${this.constructor.name}] onConnect: Resources active`);
    }

    onDisconnect() {
        console.log(`[${this.constructor.name}] onDisconnect: Cleaning up`);
    }

    // --- The "Hub" Replacement Logic ---

    /**
     * Binds an RxJS Observable to a reactive property.
     * Replaces manual LifecycleHub instantiation.
     * @param {Observable} source$ 
     * @param {*} initialValue 
     * @returns {{value: *}} A reactive box
     */
    bind(source$, initialValue = null) {
        const state = { value: initialValue };

        source$.pipe(
            takeUntil(this.destroy$) // Auto-kill when VM disconnects
        ).subscribe({
            next: (val) => {
                state.value = val;
                this.host.requestUpdate(); // Tell Lit to re-render
            },
            error: (err) => console.error(`[${this.constructor.name}] Binding Error:`, err)
        });

        return state;
    }
}