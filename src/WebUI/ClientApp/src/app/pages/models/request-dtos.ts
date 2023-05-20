export interface RequestDto {
    courseName: string;
    id: number;
    examName?: string;
    requestType: string;
    requestStatus: string;
    dueDate: string;
    materialName?: string;
    studentName?: string;
    description: string;
    hasRequested: boolean;
    hasFeedback: boolean;
}