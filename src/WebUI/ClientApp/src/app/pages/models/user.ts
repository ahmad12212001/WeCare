export interface User {
    id: string;
    studentId: string;
    firstName: string;
    lastName: string;
    major: string;
    role: string;
    email: string;
    phoneNumber?: string;
}