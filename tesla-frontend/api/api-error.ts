/*
 * Ems.WebApp.Front
 *
 * (c) 2022 Ejsak Gorup
 */
export class APIError extends Error {
  response: any;
  constructor(message: string, response: unknown) {
    super(message);
    this.name = "APIError";
    this.response = response;
  }
}

export type ErrorType = {
  status: boolean;
  errorSimpleMessage: string;
  data: {
    code: string;
    message: string;
    messageData: any;
  }[];
};
