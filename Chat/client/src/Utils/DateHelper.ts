export const getTime = function (time: string): string {
    const date = new Date(Date.parse(time))
    const hours: string = date.getHours().toString()
    const minutes: string = date.getHours().toString()
    return ((hours.length === 1) ? ("0" + hours) : hours) + ":" + ((minutes.length === 1) ? ("0" + minutes) : minutes)
}