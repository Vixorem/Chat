import {IRequestParam} from "../Utils/IRequestParam";

export class QueryRepository {
    static async getFromServer<T>(host: string, controller: string, ...params: IRequestParam[]): Promise<T> {
        let url: string = `${host}/${controller}/?`
        params.forEach(param => url += `${param.name}=${param.value}&`)
        const response = await fetch(url)
        let a = await response.json()
        return a as T;
    }

    static async postToServer<T>(host: string, controller: string, body: string): Promise<T> {
        const url: string = `${host}/${controller}/`
        console.log(body)
        const response = await fetch(url, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: body
        })

        return await response.json() as T
    }
}