export enum UserRole {
  None = 0,
  Admin = 3,
  Customer = 1,
  Manager = 2
}

export const UserRoleNames = {
  [UserRole.None]: 'Nenhuma',
  [UserRole.Admin]: 'Administrador',
  [UserRole.Customer]: 'Usuário',
  [UserRole.Manager]: 'Gerente'
};
