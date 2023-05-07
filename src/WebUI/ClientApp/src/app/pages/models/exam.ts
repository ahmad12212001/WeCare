export interface Exam {
    id: number;
    name?: string;
    hallNo: string;
    dueDate: Date;
    location?: string;
    courseId: number;
}