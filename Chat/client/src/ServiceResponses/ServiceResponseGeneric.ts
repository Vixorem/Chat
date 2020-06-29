import {ServiceResponse} from "./ServiceResponse";

export class ServiceResponseGeneric<T> extends ServiceResponse {
    value: T

    constructor(val: T, resultType: number, errorMessage: string) {
        super(resultType, errorMessage);
        this.value = val
    }
}