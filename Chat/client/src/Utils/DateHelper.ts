export const getTime = function (time: string): string {
    return new Date(Date.parse(time)).getHours() + ":" + new Date(Date.parse(time)).getMinutes()
}