export class ServiceResponse {
    resultType: string = ""
    errorMessage: string = ""
    
    constructor(resultType: string, errorMessage: string) {
        this.resultType = resultType
        this.errorMessage = errorMessage
    }
}