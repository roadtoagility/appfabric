export class Project{
    id: string;
    name: string;
    code: string;
    startDate: Date = new Date();
    budget: number;
    clientId: string;
    isFavorited: boolean = false;

    status: number;
    hourCost: number;
    profitMargin: number;
    manager: string;
    orderNumber: string;

    constructor(project?){
        this.update(project);
    }

    update(project){
        this.id = project.id === null || project.id === undefined ? this.id : project.id;
        this.name = project.name === null || project.name === undefined ? this.name : project.name;
        this.code = project.code === null || project.code === undefined ? this.code : project.code;
        this.startDate = project.startDate === null || project.startDate === undefined ? this.startDate : project.startDate;
        this.budget = project.budget === null || project.budget === undefined ? this.budget : +project.budget;
        this.clientId = project.clientId === null || project.clientId === undefined ? this.clientId : project.clientId;
        this.isFavorited = project.isFavorited === null || project.isFavorited === undefined ? this.isFavorited : project.isFavorited;

        this.status = project.status === null || project.status === undefined ? this.status : +project.status;
        this.hourCost = project.hourCost === null || project.hourCost === undefined ? this.hourCost : +project.hourCost;
        this.profitMargin = project.profitMargin === null || project.profitMargin === undefined ? this.profitMargin : project.profitMargin;
        this.manager = project.manager === null || project.manager === undefined ? this.manager : project.manager;
        this.orderNumber = project.orderNumber === null || project.orderNumber === undefined ? this.orderNumber : project.orderNumber;
    }
}