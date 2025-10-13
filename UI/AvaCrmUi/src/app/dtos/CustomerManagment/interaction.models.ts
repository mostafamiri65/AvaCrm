// interaction.models.ts
export enum InteractionType {
  Call = 1,
  Email = 2,
  Meeting = 3,
  Sms = 4,
  Online = 5
}

export interface InteractionListDto {
  id: number;
  customerId: number;
  interactionType: InteractionType;
  subject: string;
  description: string;
  nextInteraction?: Date;
  createdDate: Date;
  customerCode: string;
  interactionTypeName: string;
}
export interface InteractionCreateDto {
  customerId: number;
  interactionType: InteractionType;
  subject: string;
  description: string;
  nextInteraction?: Date;
}

export interface InteractionUpdateDto {
  id: number;
  interactionType: InteractionType;
  subject: string;
  description: string;
  nextInteraction?: Date;
}
