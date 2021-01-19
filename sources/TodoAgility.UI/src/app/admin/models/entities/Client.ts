export class Client{
    name: string;
    cnpj: string;
    email: string;

    constructor(client?){
        this.name = client.name || '';
        this.cnpj = client.cnpj || '';
        this.email = client.email || '';
    }
}