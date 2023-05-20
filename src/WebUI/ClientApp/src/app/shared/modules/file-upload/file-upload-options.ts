import { ActionControl } from "@shared/models/action-control";

export interface FileUploadOptions {
    isRequired: boolean;
    isMulti: boolean;
    error: string;
    allowedExtensions?: string[];
    progress$?: ActionControl;
}