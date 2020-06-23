import {UserDto} from "./UserDto";

export class MessageDto {
    id: string = ""
    messageType: string
    content: string
    sender: UserDto
    receiverId: string
    sentTime: Date

    constructor(messageType: string, content: string, sender: UserDto, receiverId: string, sentTime: string) {
        this.messageType = messageType
        this.content = content
        this.sender = sender
        this.receiverId = receiverId
        this.sentTime = new Date(Date.parse(sentTime))
    }
}