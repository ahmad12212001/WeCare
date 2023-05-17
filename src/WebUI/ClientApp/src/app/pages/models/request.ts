import { RequestStatus } from "../enums/request-status";
import { RequestType } from "../enums/request-type";


export interface Request {
    id?: number;
    dueDate: string;
    requestType: RequestType;
    requestStatus?: RequestStatus;
    rate?: number;
    courseId: number;
    materialId?: number;
    examId?: number;
    disabilityStudentId?: number;
    assignedVolunteerStudentId?: number;
    description: string;
}
