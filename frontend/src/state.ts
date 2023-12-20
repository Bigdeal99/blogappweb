import {Injectable} from "@angular/core";
import {Blog} from "./models";

@Injectable({
  providedIn: 'root'
})
export class State {
  books: Blog[] = [];
}
