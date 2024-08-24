class Translation {
    constructor(translate) {
        this.translate = translate;
    }

    getTranslate(key) {
        const translate = this.translate[key];
        
        return translate !== undefined ? translate : key;
    }
}

export default Translation;