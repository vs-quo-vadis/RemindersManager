export interface Reminder {
    id: string,
    subject: string,
    notes: string,
    remindDate: Date,
    isActive: boolean,
    isCancelled: boolean
  }