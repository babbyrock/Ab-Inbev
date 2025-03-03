import { UserRole } from "../enums/user-role";
import { UserStatus } from "../enums/user-status";

export interface User {
  id?: string;
  username: string;
  email: string;
  phone: string;
  password: string;
  role: UserRole;
  status: UserStatus;
  createdAt?: string;
  updatedAt?: string | null;
}
