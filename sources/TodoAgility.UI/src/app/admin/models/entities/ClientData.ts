export class ClientData {
    
    id: number = 0;
    name: string;
    cnpj: string;
    commercialEmail: string;

    address: string;
    state: string;
    city: string;

    cep: string;
    businessArea: string;
    businessManager: string;
    commercialPhone: string;

    constructor(client?){
        this.update(client);
    }

    update(client){
        this.id = client.id === null || client.id === undefined ? this.id : +client.id;
        this.name = client.name === null || client.name === undefined ? this.name : client.name;
        this.cnpj = client.cnpj === null || client.cnpj === undefined ? this.cnpj : client.cnpj;
        this.commercialEmail = client.commercialEmail === null || client.commercialEmail === undefined ? this.commercialEmail : client.commercialEmail;

        this.address = client.address === null || client.address === undefined ? this.address : client.address;
        this.state = client.state === null || client.state === undefined ? this.state : client.state;
        this.city = client.city === null || client.city === undefined ? this.city : client.city;

        this.cep = client.cep === null || client.cep === undefined ? this.id : client.cep;
        this.businessArea = client.businessArea === null || client.businessArea === undefined ? this.businessArea : client.businessArea;
        this.businessManager = client.businessManager === null || client.businessManager === undefined ? this.businessManager : client.businessManager;
        this.commercialPhone = client.commercialPhone === null || client.commercialPhone === undefined ? this.commercialPhone : client.commercialPhone;
    }
}