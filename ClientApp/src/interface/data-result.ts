import { ResultStatus } from "src/common/enum/result-status";

export class DataResultWithT<T> {
  status: ResultStatus;
  message: string;
  data: T;
}

export class DataResult {
  status: ResultStatus;
  message: string;
  resultId: number;
}
