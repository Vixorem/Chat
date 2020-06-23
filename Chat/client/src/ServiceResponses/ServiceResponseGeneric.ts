import {ServiceResponse} from "./ServiceResponse";

export class ServiceResponseGeneric<T> extends ServiceResponse {
    value: T

    constructor(val: T, resultType: string, errorMessage: string) {
        super(resultType, errorMessage);
        this.value = val
    }
}