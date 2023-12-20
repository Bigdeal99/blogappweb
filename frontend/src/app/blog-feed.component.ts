import { Component, OnInit } from "@angular/core";
import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { environment } from "../environments/environment";
import { firstValueFrom } from "rxjs";
import { Blog, ResponseDto } from "../models";
import { State } from "../state";
import { ModalController, ToastController } from "@ionic/angular";
import { CreateBlogComponent } from "./create-blog.component";

@Component({
  template: `
    <ion-content>
      <ion-list>
        <ion-card *ngFor="let blog of state.blog">
          <ion-card-header>
            <ion-card-title>{{blog.title}}</ion-card-title>
          </ion-card-header>
          <ion-card-content>
            <p>{{blog.summary}}</p>
            <img [src]="blog.featuredImage" *ngIf="blog.featuredImage" />
            <ion-button (click)="blog.id !== undefined && deleteBlog(blog.id)">Delete</ion-button>
          </ion-card-content>
        </ion-card>
      </ion-list>

      <ion-fab vertical="bottom" horizontal="end" slot="fixed">
        <ion-fab-button (click)="openModal()">
          <ion-icon name="add"></ion-icon>
        </ion-fab-button>
      </ion-fab>
    </ion-content>
  `
})
export class BlogFeed implements OnInit {
  constructor(
    public http: HttpClient,
    public modalController: ModalController,
    public state: State,
    public toastController: ToastController
  ) {}

  async fetchBlogs() {
    try {
      const result = await firstValueFrom(this.http.get<ResponseDto<Blog[]>>(`${environment.baseUrl}/api/blog`));
      this.state.blog = result.responseData || [];
      // You may need to fetch each blog's comments and category separately if they are not included
    } catch (error) {
      this.showToast('Error fetching blogs', 'danger');
    }
  }

  ngOnInit(): void {
    this.fetchBlogs();
  }

  async deleteBlog(blogId: number) {
    try {
      await firstValueFrom(this.http.delete(`${environment.baseUrl}/api/blog/${blogId}`));
      this.state.blog = this.state.blog.filter(blog => blog.id !== blogId);
      this.showToast('Blog deleted successfully', 'success');
    } catch (error) {
      this.showToast('Error deleting blog', 'danger');
    }
  }

  async openModal() {
    const modal = await this.modalController.create({
      component: CreateBlogComponent
    });
    await modal.present();
    await modal.onDidDismiss();
    this.fetchBlogs(); // Refresh the list after adding a new blog
  }

  private async showToast(message: string, color: 'success' | 'danger') {
    const toast = await this.toastController.create({
      message,
      duration: 2000,
      color
    });
    await toast.present();
  }
}
