export class MessagePreviewDto {
    chatId:string 
    chatName : string
    lastMessage : string 
    sentTime : Date
    
    constructor(chatId: string, chatName: string, lastMessage: string, sentTime: string) {
        this.chatId = chatId
        this.chatName = chatName
        this.lastMessage = lastMessage
        this.sentTime = new Date(Date.parse(sentTime))
    }
}