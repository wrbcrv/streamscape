import { MyList } from "./my-list.model";

export interface User {
  id: number;
  email: string;
  username: string;
  role: string;
  myList: MyList[]
  createdAt: string;
}