import { Component } from "@angular/core";
import { FormBuilder, Validators } from "@angular/forms";
import { Blog, ResponseDto } from "../models";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { environment } from "../environments/environment";
import { State } from "../state";
import { firstValueFrom } from "rxjs";
import { ModalController, ToastController } from "@ionic/angular";

@Component({
  template: `
    <ion-header>
      <ion-toolbar>
        <ion-title>Create Blog</ion-title>
        <ion-buttons slot="primary">
          <ion-button (click)="dismissModal()">Close</ion-button>
        </ion-buttons>
      </ion-toolbar>
    </ion-header>
    <ion-content>
      <form [formGroup]="createBlogForm" (ngSubmit)="submit()">
        <ion-list>
          <ion-item>
            <ion-label position="floating">Title</ion-label>
            <ion-input formControlName="title"></ion-input>
          </ion-item>
          <div *ngIf="createBlogForm.controls.title.invalid && createBlogForm.controls.title.touched">
            Blog title must be 4 characters or more
          </div>

          <!-- Additional form fields for summary, content, etc. -->

          <ion-item>
            <ion-label position="floating">Summary</ion-label>
            <ion-textarea formControlName="summary"></ion-textarea>
          </ion-item>

          <ion-item>
            <ion-label position="floating">Content</ion-label>
            <ion-textarea formControlName="content"></ion-textarea>
          </ion-item>

          <!-- You can add more fields as required -->

          <ion-button type="submit" expand="full" [disabled]="createBlogForm.invalid">Create Blog</ion-button>
        </ion-list>
      </form>
    </ion-content>
  `
})
export class CreateBlogComponent {
  createBlogForm = this.fb.group({
    title: ['', [Validators.required, Validators.minLength(4)]],
    summary: ['', Validators.required],
    content: ['', Validators.required],
    publicationDate: ['', Validators.required],
    categoryId: [null, Validators.required],
    featuredImage: ['', Validators.required]
  });

  constructor(
    public fb: FormBuilder,
    public modalController: ModalController,
    public http: HttpClient,
    public state: State,
    public toastController: ToastController
  ) {}

  async submit() {
    if (this.createBlogForm.valid) {
      try {
        const response = await firstValueFrom(this.http.post<ResponseDto<Blog>>(environment.baseUrl + '/api/blog', this.createBlogForm.value));
        this.state.blog.push(response.responseData!);
        this.showToast('Blog created successfully', 'success');
        this.dismissModal();
      } catch (error) {
        this.showToast('Error creating blog', 'danger');
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
