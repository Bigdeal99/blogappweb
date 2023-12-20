// update-blog.component.ts
import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { environment } from "../environments/environment";
import { Blog, ResponseDto } from "../models";
import { ModalController, ToastController } from "@ionic/angular";

@Component({
    selector: 'app-update-blog',
    template: `
        <ion-header>
            <ion-toolbar>
                <ion-title>Update Blog</ion-title>
                <ion-buttons slot="primary">
                    <ion-button (click)="dismissModal()">Close</ion-button>
                </ion-buttons>
            </ion-toolbar>
        </ion-header>
        <ion-content>
            <form [formGroup]="updateBlogForm" (ngSubmit)="submit()">
                <ion-list>
                    <ion-item>
                        <ion-label position="floating">Title</ion-label>
                        <ion-input formControlName="title"></ion-input>
                    </ion-item>
                    <ion-item>
                        <ion-label position="floating">Summary</ion-label>
                        <ion-textarea formControlName="summary"></ion-textarea>
                    </ion-item>
                    <ion-item>
                        <ion-label position="floating">Content</ion-label>
                        <ion-textarea formControlName="content"></ion-textarea>
                    </ion-item>
                    <!-- Add other fields as necessary -->
                    <ion-button type="submit" expand="full" [disabled]="updateBlogForm.invalid">Update Blog</ion-button>
                </ion-list>
            </form>
        </ion-content>
    `
})
export class UpdateBlogComponent {
    updateBlogForm = this.fb.group({
        title: ['', [Validators.required, Validators.minLength(4)]],
        summary: ['', Validators.required],
        content: ['', Validators.required],
        // Add other fields as necessary
    });

    private blog: Blog | undefined;

    constructor(private fb: FormBuilder, private http: HttpClient, private modalController: ModalController, private toastController: ToastController) {}

    setInitialValues(blog: Blog) {
        this.blog = blog;
        this.updateBlogForm.patchValue(blog);
    }

    async submit() {
        if (this.updateBlogForm.valid && this.blog?.id) {
            try {
                const updatedBlog = await this.http.put<ResponseDto<Blog>>(`${environment.baseUrl}/api/blog/${this.blog.id}`, this.updateBlogForm.value).toPromise();
                this.showToast('Blog updated successfully', 'success');
                this.dismissModal();
            } catch (error) {
                this.showToast('Error updating blog', 'danger');
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

    async dismissModal() {
        await this.modalController.dismiss();
    }
}
