// models/captcha.model.ts
export interface CaptchaChallenge {
  id: string;
  question: string;
  answer: string;
  options: string[];
  type: CaptchaType;
  expiresAt: string;
}

export enum CaptchaType {
  Text = 'Text',
  Math = 'Math',
  Logic = 'Logic',
  Sequence = 'Sequence',
  ImageRecognition = 'ImageRecognition'
}

export interface CaptchaRequest {
  captchaId: string;
  userAnswer: string;
}

export interface CaptchaResponse {
  success: boolean;
  message: string;
  challenge?: CaptchaChallenge;
}

export interface SecurityCheckRequest {
  username: string;
  ipAddress: string;
}

export interface SecurityCheckResult {
  requiresCaptcha: boolean;
  isBlocked: boolean;
  message: string;
  failedAttempts: number;
}
