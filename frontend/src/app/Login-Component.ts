import { Component } from "@angular/core";
import { FormBuilder, FormGroup, Validators } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { environment } from "../environments/environment";
import { ToastController } from "@ionic/angular";
import { Admin, ResponseDto } from "../models";


@Component({
    selector: 'app-login',
    template: `
        <ion-header>
            <ion-toolbar>
                <ion-title>Login</ion-title>
            </ion-toolbar>
        </ion-header>
        <ion-content>
            <form [formGroup]="loginForm" (ngSubmit)="submit()">
                <ion-list>
                    <ion-item>
                        <ion-label position="floating">Username</ion-label>
                        <ion-input formControlName="username"></ion-input>
                    </ion-item>
                    <ion-item>
                        <ion-label position="floating">Password</ion-label>
                        <ion-input type="password" formControlName="password"></ion-input>
                    </ion-item>
                    <ion-button type="submit" expand="full" [disabled]="loginForm.invalid">Login</ion-button>
                </ion-list>
            </form>
        </ion-content>
    `
})
export class LoginComponent {
    loginForm: FormGroup;

    constructor(
        private fb: FormBuilder,
        private http: HttpClient,
        private toastController: ToastController
    ) {
        this.loginForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    async submit() {
        if (this.loginForm.valid) {
            try {
                const response = await this.http.post(`${environment.baseUrl}/api/admin/login`, this.loginForm.value).toPromise();
                // Handle successful login, such as storing authentication tokens or redirecting
                this.showToast('Login successful', 'success');
                // Redirect to admin dashboard or other appropriate action
            } catch (error) {
                this.showToast('Login failed', 'danger');
            }
        }
    }

    async showToast(message: string, color: 'success' | 'danger') {
        const toast = await this.toastController.create({
            message,
            duration: 2000,
            color
        });
        await toast.present();
    }
}
