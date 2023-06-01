export interface Student {
    id: number;
    studentId: string;
    firstName: string;
    lastName: string;
    major: string;
    type: string | number;
    email: string;
    phoneNumber?: string;
    courses?: string;
}