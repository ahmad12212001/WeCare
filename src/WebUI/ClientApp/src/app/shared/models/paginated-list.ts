export class PaginatedList<T>{
    items: Array<T> = [];
    pageNumber: number = 0;
    totalPages: number = 10;
    totalCount: number = 0;
    pageSize: number = 10;

    constructor(items: Array<T>, count: number, totalPages: number, totalCount: number, pageSize: number) {
        this.items = items;
        this.totalCount = count;
        this.totalPages = totalPages;
        this.totalCount = totalCount;
        this.pageSize = pageSize;
    }
}