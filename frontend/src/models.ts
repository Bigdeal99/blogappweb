export class Blog {
  bookTitle?: string;
  publisher?: string;
  bookId?: number;
  coverImgUrl?: string;
  author?: string;
}

export class ResponseDto<T> {
  responseData?: T;
  messageToClient?: string;
}
