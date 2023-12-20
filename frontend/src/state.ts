import {Injectable} from "@angular/core";
import {Admin, Blog,Comment, Category} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  blog: Blog[] = [];
  admin: Admin[] = [];
  category: Category[] = [];
  comment: Comment[] = [];}
