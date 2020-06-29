import {ResultType} from "./ResultType";

export class ServiceResponse {
    resultType: ResultType = ResultType.Ok
    errorMessage: string = ""
    
    constructor(resultType: ResultType, errorMessage: string) {
        this.resultType = resultType
        this.errorMessage = errorMessage
    }
}