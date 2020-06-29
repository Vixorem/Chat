import {UserDto} from "./UserDto";

export enum Status {
    Sent = 0,
    Received = 1,
    Error
}

export class MessageDto {
    id: string = ""
    content: string = ""
    sender: UserDto = new UserDto("", "")
    receiverId: string = ""
    sentTime: Date = new Date()
    status: Status = Status.Sent
    clientMessageId: string
    
    constructor(content: string, sender: UserDto, receiverId: string, clientMessageId: string, sentTime: Date = new Date(), status: Status = Status.Sent) {
        this.content = content
        this.sender = sender
        this.receiverId = receiverId
        this.sentTime = sentTime
        this.status = status
        this.clientMessageId = clientMessageId
    }
}