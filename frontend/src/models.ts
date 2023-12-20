export interface Blog {
  id: number;
  title?: string;
  summary?: string;
  content?: string;
  publicationDate?: Date;
  categoryId?: number;
  category?: Category;
  comments?: Comment[];
  featuredImage?: string;

}

export interface Admin {
  id: number;
  username: string;
  passwordHash: string; // Note: Be careful with handling passwords on the frontend.
  failedLoginAttempts: number;
}
export interface Category {
  id: number;
  name: string;
  description: string;
  blogs?: Blog[]; // Assuming you have a Blog interface. Marked as optional.
}

export interface Comment {
  id: number;
  name: string;
  email: string;
  text: string;
  publicationDate: Date; // or string if you handle dates as strings
  blogId: number;
  blog?: Blog; // Optional, depending on your use case.
}

export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
