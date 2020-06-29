export class UserDto {
    id: string = ""
    login: string = ""
    
    constructor(id: string, login: string) {
        this.id = id
        this.login = login
    }
}