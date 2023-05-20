export interface MaterialDto {
    id: number;
    name: string;
    description: string;
    path: string;
    contentType: string;
    courseId: number;
    materialStatus: string;
    requestId?: number;
    volunteerStudentId?: number;
    courseName: string;
}