class States {
    states: any[];

    constructor() {
        this.states = [
            { name: 'Acre', shortcut: 'AC' },
            { name: 'Alagoas', shortcut: 'AL' },
            { name: 'Amapá', shortcut: 'AP' },
            { name: 'Amazonas', shortcut: 'AM' },
            { name: 'Bahia', shortcut: 'BA' },
            { name: 'Ceará', shortcut: 'CE' },
            { name: 'Distrito Federal', shortcut: 'DF' },
            { name: 'Espírito Santo', shortcut: 'ES' },
            { name: 'Goiás', shortcut: 'GO' },
            { name: 'Maranhão', shortcut: 'MA' },
            { name: 'Mato Grosso', shortcut: 'MT' },
            { name: 'Mato Grosso do Sul', shortcut: 'MS' },
            { name: 'Minas Gerais', shortcut: 'MG' },
            { name: 'Pará', shortcut: 'PA' },
            { name: 'Paraíba', shortcut: 'PB' },
            { name: 'Paraná', shortcut: 'PR' },
            { name: 'Pernambuco', shortcut: 'PE' },
            { name: 'Piauí', shortcut: 'PI' },
            { name: 'Rio de Janeiro', shortcut: 'RJ' },
            { name: 'Rio Grande do Norte', shortcut: 'RN' },
            { name: 'Rio Grande do Sul', shortcut: 'RS' },
            { name: 'Rondônia', shortcut: 'RO' },
            { name: 'Roraima', shortcut: 'RR' },
            { name: 'Santa Catarina', shortcut: 'SC' },
            { name: 'São Paulo', shortcut: 'SP' },
            { name: 'Sergipe', shortcut: 'SE' },
            { name: 'Tocantins', shortcut: 'TO' },
        ];
    }

    getCidadesByshortcut(shortcut): any[] {
        return this.states.filter(
            row => row.shortcut === shortcut
        );
    }

    getEstados(): any[] {
        return this.states;
    }
}


export { States }; 
