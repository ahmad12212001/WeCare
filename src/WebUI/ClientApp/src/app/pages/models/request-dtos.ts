export interface RequestDto {
    courseName: string;
    id: number;
    examName: string | null;
    requestType: string;
    requestStatus: string;
    dueDate: string;
    materialName: string | null;
    volunteerName: string | null;
    description: string;
}