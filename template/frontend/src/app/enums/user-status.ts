export enum UserStatus {
  Unknown = 0,
  Active = 1,
  Inactive = 2,
  Suspended = 3
}

export const UserStatusNames = {
  [UserStatus.Unknown]: 'Desconhecido',
  [UserStatus.Active]: 'Ativo',
  [UserStatus.Inactive]: 'Inativo',
  [UserStatus.Suspended]: 'Suspenso'
};
