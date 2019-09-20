export interface Login {
  userName: string;
  password: string;
  rememberMe: boolean;
}
export interface News {
  title: string;
  content: string;
  created: string;
  id: number;
}
export interface ShowMessage {
  message: string;
  email: string;
  fullName: string;
  subject: string;
  id: number;
  created: string;
}
