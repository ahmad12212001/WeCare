export interface Material {
    name: string;
    description: string;
    courseId: number;
    requestId?: number;
    file: File;
}