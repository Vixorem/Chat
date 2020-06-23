import {ServiceResponse} from "../ServiceResponses/ServiceResponse";
import {ServiceResponseGeneric} from "../ServiceResponses/ServiceResponseGeneric";

export function errorAlert(response: ServiceResponse): void {
    if (response.resultType === "2") {
        alert("Ошибка сервера")
    } else if (response.resultType === "3") {
        alert("Ошибка: " + response.errorMessage)
    }
}

export function errorAlertGeneric<T>(response: ServiceResponseGeneric<T>): void {
    errorAlert(new ServiceResponse(response.resultType, response.errorMessage))
}