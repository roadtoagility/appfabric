export class Client{
    name: string;
    cnpj: string;
    email: string;

    constructor(client?){
        this.name = client.name || null;
        this.cnpj = client.cnpj || null;
        this.email = client.email || null;
    }
}