import {Injectable} from "@angular/core";
import {Blog,Admin,Category,Comment} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  blog: Blog[] = [];
  admin: Admin[] = [];
  category: Category[] = [];
  comment: Comment[] = [];
}
