export class Project{
    name: string;
    code: string;
    startDate: Date;
    budget: number;
    clientId: number;

    constructor(project?){
        this.name = project.name || '';
        this.code = project.code || '';
        this.startDate = project.startDate || new Date();
        this.budget = project.budget || 0;
        this.clientId = project.clientId || 0;
    }
}