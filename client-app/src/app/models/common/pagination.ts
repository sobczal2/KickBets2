export interface PaginatedRequestData {
    pageSize: number | null;
    currentPage: number | null;
}

export interface PaginatedResponse<T> {
    pageSize: number;
    currentPage: number;
    totalResults: number;
    totalPages: number;
    items: T[];
}

export interface PaginatedResponseData {
    pageSize: number;
    currentPage: number;
    totalResults: number;
    totalPages: number;
}