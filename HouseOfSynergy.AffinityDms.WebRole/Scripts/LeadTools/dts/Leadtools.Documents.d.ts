/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="Leadtools.d.ts" />
/// <reference path="Leadtools.Annotations.Core.d.ts" />
declare module lt.Documents.Barcode {
    enum BarcodeSymbology {
        unknown = 0,
        ean13 = 1,
        ean8 = 2,
        upca = 3,
        upce = 4,
        code3Of9 = 5,
        code128 = 6,
        codeInterleaved2Of5 = 7,
        codabar = 8,
        uccean128 = 9,
        code93 = 10,
        eanext5 = 11,
        eanext2 = 12,
        msi = 13,
        code11 = 14,
        codeStandard2Of5 = 15,
        gs1Databar = 16,
        gs1DatabarLimited = 17,
        gs1DatabarExpanded = 18,
        patchCode = 19,
        postNet = 20,
        planet = 21,
        australianPost4State = 22,
        royalMail4State = 23,
        usps4State = 24,
        gs1DatabarStacked = 25,
        gs1DatabarExpandedStacked = 26,
        pdf417 = 27,
        microPDF417 = 28,
        datamatrix = 29,
        qr = 30,
        aztec = 31,
        maxi = 32,
        microQR = 33,
    }
    class BarcodeData {
        constructor();
        static fromJson(obj: any): BarcodeData;
        copyFromJson(obj: any): boolean;
        private _symbology;
        symbology: BarcodeSymbology;
        private _bounds;
        bounds: LeadRectD;
        private _rotationAngle;
        rotationAngle: number;
        private _data;
        data: number[];
        private _value;
        value: string;
    }
}
declare module lt.Documents {
    enum DocumentConverterSvgImagesRecognitionMode {
        auto = 0,
        disabled = 1,
        always = 2,
    }
    enum DocumentConverterEmptyPageMode {
        none = 0,
        skip = 1,
        skipIgnoreAnnotations = 2,
    }
    enum DocumentConverterJobErrorMode {
        abort = 0,
        resume = 1,
    }
    enum DocumentConverterAnnotationsMode {
        none = 0,
        external = 1,
        overlay = 2,
        embed = 3,
    }
    class DocumentConverterJobData {
        constructor();
        jobErrorMode: DocumentConverterJobErrorMode;
        pageNumberingTemplate: string;
        enableSvgConversion: boolean;
        svgImagesRecognitionMode: DocumentConverterSvgImagesRecognitionMode;
        emptyPageMode: DocumentConverterEmptyPageMode;
        preprocessorDeskew: boolean;
        preprocessorOrient: boolean;
        preprocessorInvert: boolean;
        inputDocumentFirstPageNumber: number;
        inputDocumentLastPageNumber: number;
        documentFormat: Writers.DocumentFormat;
        rasterImageFormat: RasterImageFormat;
        rasterImageBitsPerPixel: number;
        documentOptions: Writers.DocumentOptions;
        jobName: string;
        annotationsMode: DocumentConverterAnnotationsMode;
        documentName: string;
        annotations: string;
    }
}
declare module lt.Documents {
    class DocumentBarcodes {
        constructor(document: Document);
        private _document;
        document: Document;
    }
}
declare module lt.Documents {
    enum RasterImageFormat {
        unknown = 0,
        tifJpeg422 = 24,
        ccittGroup4 = 29,
        rasPdfJpeg = 150,
        rasPdfG4 = 149,
    }
}
declare module lt.Documents.Writers {
    class DocumentOptions {
        constructor(format: DocumentFormat);
        private _format;
        format: DocumentFormat;
    }
}
declare module lt.Documents.Writers {
    class RtfDocumentOptions extends DocumentOptions {
        constructor();
        textMode: DocumentTextMode;
    }
}
declare module lt.Documents.Writers {
    enum TextDocumentType {
        ansi = 0,
        unicode = 1,
        unicodeBigEndian = 2,
        utf8 = 3,
    }
    class TextDocumentOptions extends DocumentOptions {
        constructor();
        documentType: TextDocumentType;
        addPageNumber: boolean;
        addPageBreak: boolean;
        formatted: boolean;
    }
}
declare module lt.Documents.Writers {
    class SvgDocumentOptions extends DocumentOptions {
        constructor();
    }
    class EmfDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    class XlsDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    enum PdfDocumentType {
        pdf = 0,
        pdfA = 1,
        pdf12 = 2,
        pdf13 = 3,
        pdf15 = 4,
        pdf16 = 5,
    }
    enum PdfDocumentEncryptionMode {
        rc40Bit = 0,
        rc128Bit = 1,
    }
    enum PdfDocumentPageModeType {
        pageOnly = 0,
        fullScreen = 3,
        bookmarksAndPage = 1,
        thumbnailAndPage = 2,
        layerAndPage = 4,
        attachmentsAndPage = 5,
    }
    enum PdfDocumentPageLayoutType {
        singlePageDisplay = 0,
        oneColumnDisplay = 1,
        twoColumnLeftDisplay = 2,
        twoColumnRightDisplay = 3,
        twoPageLeft = 4,
        twoPageRight = 5,
    }
    enum PdfDocumentPageFitType {
        defaultType = 0,
        fitWidth = 1,
        fitHeight = 2,
        fitWidthBounds = 3,
        fitHeightBounds = 4,
        fitBounds = 5,
    }
    class PdfDocumentOptions extends DocumentOptions {
        constructor();
        documentType: PdfDocumentType;
        fontEmbedMode: DocumentFontEmbedMode;
        imageOverText: boolean;
        linearized: boolean;
        title: string;
        subject: string;
        keywords: string;
        author: string;
        isProtected: boolean;
        userPassword: string;
        ownerPassword: string;
        encryptionMode: PdfDocumentEncryptionMode;
        printEnabled: boolean;
        highQualityPrintEnabled: boolean;
        copyEnabled: boolean;
        editEnabled: boolean;
        annotationsEnabled: boolean;
        assemblyEnabled: boolean;
        oneBitImageCompression: OneBitImageCompressionType;
        coloredImageCompression: ColoredImageCompressionType;
        qualityFactor: number;
        imageOverTextSize: DocumentImageOverTextSize;
        imageOverTextMode: DocumentImageOverTextMode;
        pageModeType: PdfDocumentPageModeType;
        pageLayoutType: PdfDocumentPageLayoutType;
        pageFitType: PdfDocumentPageFitType;
        initialPageNumber: number;
        xCoordinate: number;
        yCoordinate: number;
        zoomPercent: number;
        hideToolbar: boolean;
        hideMenubar: boolean;
        hideWindowUI: boolean;
        fitWindow: boolean;
        centerWindow: boolean;
        displayDocTitle: boolean;
    }
}
declare module lt.Documents.Writers {
    class XpsDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    class PubDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    class MobDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    class LtdDocumentOptions extends DocumentOptions {
        constructor();
    }
}
declare module lt.Documents.Writers {
    class HtmlDocumentOptions extends DocumentOptions {
        constructor();
        fontEmbedMode: DocumentFontEmbedMode;
        useBackgroundColor: boolean;
        backgroundColor: string;
    }
}
declare module lt.Documents.Writers {
    class DocxDocumentOptions extends DocumentOptions {
        constructor();
        textMode: DocumentTextMode;
    }
}
declare module lt.Documents.Writers {
    class DocDocumentOptions extends DocumentOptions {
        constructor();
        textMode: DocumentTextMode;
    }
}
declare module lt.Documents.Writers {
    enum AltoXmlMeasurementUnit {
        mm10 = 0,
        inch1200 = 1,
        dpi = 2,
        pixel = 3,
    }
    class AltoXmlDocumentOptions extends DocumentOptions {
        constructor();
        measurementUnit: AltoXmlMeasurementUnit;
        fileName: string;
        processingDateTime: string;
        processingAgency: string;
        processingStepDescription: string;
        processingStepSettings: string;
        softwareCreator: string;
        softwareName: string;
        softwareVersion: string;
        applicationDescription: string;
        firstPhysicalPageNumber: number;
        formatted: boolean;
        indentation: string;
    }
}
declare module lt.Documents {
    class DocumentAnnotations {
        constructor(document: Document);
        private _document;
        document: Document;
        annotationsUri: string;
        private static _className;
        private static _controllerName;
        getAnnotations(createEmpty: boolean): JQueryPromise<lt.Annotations.Core.AnnContainer[]>;
        setAnnotations(containers: lt.Annotations.Core.AnnContainer[]): JQueryPromise<void>;
    }
}
declare module lt.Documents.Writers {
    enum DocumentFormat {
        user = -1,
        ltd = 0,
        pdf = 1,
        doc = 2,
        rtf = 3,
        html = 4,
        text = 5,
        emf = 6,
        xps = 7,
        docx = 8,
        xls = 9,
        pub = 10,
        mob = 11,
        svg = 12,
        altoXml = 13,
    }
    enum DocumentFontEmbedMode {
        none = 0,
        auto = 1,
        force = 2,
        all = 3,
    }
    enum OneBitImageCompressionType {
        flate = 0,
        faxG31D = 1,
        faxG32D = 2,
        faxG4 = 3,
        lzw = 4,
        jbig2 = 5,
    }
    enum ColoredImageCompressionType {
        flateJpeg = 0,
        lzwJpeg = 1,
        flate = 2,
        lzw = 3,
        jpeg = 4,
        flateJpx = 5,
        lzwJpx = 6,
        jpx = 7,
    }
    enum DocumentImageOverTextSize {
        original = 0,
        half = 1,
        quarter = 2,
    }
    enum DocumentImageOverTextMode {
        none = 0,
        strict = 1,
        relaxed = 2,
    }
    enum DocumentTextMode {
        auto = 0,
        nonFramed = 1,
        framed = 2,
    }
}
declare module lt.Documents {
    class DocumentPagesArrayWrapper {
        constructor();
        pop(): number;
        push(val: any): number;
        length: number;
    }
    class DocumentPages extends DocumentPagesArrayWrapper {
        constructor(document: Document);
        private _document;
        document: Document;
        defaultPageSize: lt.LeadSizeD;
        defaultResolution: number;
    }
}
declare module lt.Documents.Internal {
    class DocumentPageValues {
        static fromJson(obj: any): DocumentPageValues;
        copyFromJson(obj: any): boolean;
        constructor();
        private _size;
        private _resolution;
        private _pageNumber;
        private _originalPageNumber;
        private _isDeleted;
        private _isAnnotationsModified;
        private _hasEmbeddedAnnotations;
        size: lt.LeadSizeD;
        resolution: number;
        pageNumber: number;
        originalPageNumber: number;
        isDeleted: boolean;
        isAnnotationsModified: boolean;
        hasEmbeddedAnnotations: boolean;
    }
}
declare module lt.Documents {
    class DocumentParseStructureData {
        DocumentParseStructureData(): void;
        private _bookmarks;
        bookmarks: DocumentBookmark[];
        private _pageLinks;
        pageLinks: DocumentLink[];
    }
    class DocumentStructure {
        private _document;
        document: Document;
        copyFromJson(bookmarksJson: any[], pageLinksJson: any[][]): boolean;
        constructor(document: Document);
        private static _className;
        private static _controllerName;
        private _bookmarks;
        bookmarks: DocumentBookmark[];
        parseBookmarks: boolean;
        parsePageLinks: boolean;
        isParsed: boolean;
        parse(): JQueryPromise<Document>;
    }
}
declare module lt.Documents {
    enum DocumentTextExtractionMode {
        auto = 0,
        svgOnly = 1,
        ocrOnly = 2,
    }
    class DocumentText {
        private _document;
        document: Document;
        static fromJson(obj: any, document: Document): DocumentText;
        copyFromJson(obj: any): boolean;
        constructor(document: Document);
        textExtractionMode: DocumentTextExtractionMode;
        autoParseLinks: boolean;
        private static _defaultLinkPatterns;
        private static _defaultLinkFlags;
        private static _linkPatterns;
        static linkPatterns: RegExp[];
        static resetLinkPatterns(): void;
    }
}
declare module lt.Documents.Internal {
    class DocumentValues {
        static fromJson(obj: any): DocumentValues;
        copyFromJson(obj: any): boolean;
        constructor();
        private __defaultQualityFactor;
        private _annotationsUri;
        private _defaultBitsPerPixel;
        private _defaultPageSize;
        private _defaultResolution;
        private _documentId;
        private _name;
        private _documentType;
        private _fileLength;
        private _isDecrypted;
        private _isDownloaded;
        private _isEncrypted;
        private _isReadOnly;
        private _isStructureParsed;
        private _isStructureSupported;
        private _mimeType;
        private _password;
        private _maximumImagePixelSize;
        private _thumbnailPixelSize;
        private _unembedSvgImages;
        private _uri;
        private _isSvgSupported;
        private _isSvgViewingPreferred;
        private _isResolutionsSupported;
        private _textExtractionMode;
        private _autoParseLinks;
        private _parseBookmarks;
        private _parsePageLinks;
        annotationsUri: string;
        defaultBitsPerPixel: number;
        defaultPageSize: lt.LeadSizeD;
        defaultResolution: number;
        documentId: string;
        name: string;
        documentType: string;
        fileLength: number;
        isDecrypted: boolean;
        isDownloaded: boolean;
        isEncrypted: boolean;
        isReadOnly: boolean;
        isStructureParsed: boolean;
        isStructureSupported: boolean;
        mimeType: string;
        password: string;
        maximumImagePixelSize: number;
        thumbnailPixelSize: lt.LeadSizeD;
        unembedSvgImages: boolean;
        uri: string;
        isSvgSupported: boolean;
        isSvgViewingPreferred: boolean;
        isResolutionsSupported: boolean;
        textExtractionMode: DocumentTextExtractionMode;
        autoParseLinks: boolean;
        parseBookmarks: boolean;
        parsePageLinks: boolean;
    }
}
declare module lt.Documents {
    class DocumentImages {
        constructor(document: Document);
        private static _className;
        private static _controllerName;
        private _document;
        document: Document;
        isSvgSupported: boolean;
        isSvgViewingPreferred: boolean;
        isResolutionsSupported: boolean;
        defaultBitsPerPixel: number;
        maximumImagePixelSize: number;
        thumbnailPixelSize: lt.LeadSizeD;
        unembedSvgImages: boolean;
        private static _elementAjaxMethod;
        static elementAjaxMethod: string;
        private static _elementUrlMode;
        static elementUrlMode: lt.ImageLoaderUrlMode;
        static getImageScale(imageSize: lt.LeadSizeD, maximumImagePixelSize: number): number;
        static scaleImageSize(imageSize: lt.LeadSizeD, imageScale: number): lt.LeadSizeD;
        private static _getThumbnailsGridActionName;
        private static _getThumbnailsGridMethodName;
        private getThumbnailsGridUrlParams(firstPageNumber, lastPageNumber, maximumGridWidth);
        getThumbnailsGrid(firstPageNumber: number, lastPageNumber: number, maximumGridWidth: number): string;
        getThumbnailsGridUrl(firstPageNumber: number, lastPageNumber: number, maximumGridWidth: number): string;
        getThumbnailsGridElement(firstPageNumber: number, lastPageNumber: number, maximumGridWidth: number, imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
    }
}
declare module lt.Documents {
    class DocumentLinkTarget {
        constructor();
        static fromJson(obj: any): DocumentLink;
        copyFromJson(obj: any): boolean;
        private _pageFitType;
        pageFitType: DocumentPageFitType;
        private _pageNumber;
        pageNumber: number;
        private _position;
        position: lt.LeadPointD;
        private _zoomPercent;
        zoomPercent: number;
        clone(): DocumentLinkTarget;
    }
}
declare module lt.Documents {
    enum DocumentLinkType {
        value = 0,
        targetPage = 1,
    }
    class DocumentLink {
        constructor();
        static fromJson(obj: any): DocumentLink;
        copyFromJson(obj: any): boolean;
        private _bounds;
        bounds: lt.LeadRectD;
        private _linkType;
        linkType: DocumentLinkType;
        private _value;
        value: string;
        private _target;
        target: DocumentLinkTarget;
    }
}
declare module lt.Documents {
    enum DocumentGetSvgOptions {
        none = 0,
        allowPolylineText = 1,
        dropImages = 2,
        dropShapes = 4,
        dropText = 8,
        forConversion = 16,
        ignoreXmlParsingErrors = 32,
    }
    class DocumentPage {
        constructor(document: Document);
        private _values;
        private _document;
        private _links;
        private static _className;
        private static _controllerName;
        static fromJson(obj: any, document: Document, pageNumber: number): DocumentPage;
        copyFromJson(obj: any, pageNumber: number): boolean;
        document: Document;
        size: lt.LeadSizeD;
        resolution: number;
        pageNumber: number;
        imageScale: number;
        originalPageNumber: number;
        isDeleted: boolean;
        isAnnotationsModified: boolean;
        hasEmbeddedAnnotations: boolean;
        values: Internal.DocumentPageValues;
        private parseAndMergeValueLinks(text);
        private removeSmallerDuplicate(links, link);
        private mergeLinks(links);
        copyLinksFromJson(objs: any[]): boolean;
        getLinks(): DocumentLink[];
        getText(clip: LeadRectD): JQueryPromise<DocumentPageText>;
        getAnnotations(createEmpty: boolean): JQueryPromise<lt.Annotations.Core.AnnContainer>;
        setAnnotations(container: lt.Annotations.Core.AnnContainer): JQueryPromise<void>;
        private static _getThumbnailImageActionName;
        private static _getThumbnailImageMethodName;
        private getThumbnailImageUrlParams();
        getThumbnailImage(): string;
        getThumbnailImageUrl(): string;
        getThumbnailImageElement(imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
        private static _getImageActionName;
        private static _getImageMethodName;
        private static _getImageResizedActionName;
        private static _getImageResizedMethodName;
        private getImageUrlParams(width, height);
        getImage(): string;
        getImageUrl(): string;
        getImageResized(width: number, height: number): string;
        getImageResizedUrl(width: number, height: number): string;
        getImageElement(imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
        getImageResizedElement(width: number, height: number, imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
        private internalGetImageElement(methodName, width, height, imageLoader?);
        private static _getSvgBackImageActionName;
        private static _getSvgBackImageMethodName;
        private getSvgBackImageUrlParams(backColor);
        getSvgBackImage(backColor: string): string;
        getSvgBackImageUrl(backColor: string): string;
        getSvgBackImageElement(backColor: string, imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
        private static _getSvgActionName;
        private static _getSvgMethodName;
        private getSvgUrlParams(options);
        getSvg(options: DocumentGetSvgOptions): string;
        getSvgUrl(options: DocumentGetSvgOptions): string;
        getSvgElement(options: DocumentGetSvgOptions, imageLoader?: lt.ImageLoader): JQueryPromise<Element>;
        readBarcodes(bounds: LeadRectD, maximumBarcodes: number, symbologies: Barcode.BarcodeSymbology[]): JQueryPromise<Barcode.BarcodeData[]>;
    }
}
declare module lt.Documents {
    enum DocumentFontStyles {
        normal = 0,
        bold = 1,
        italic = 2,
        underline = 4,
    }
    enum DocumentPageFitType {
        none = 0,
        fit = 1,
        fitAlways = 2,
        fitWidth = 3,
        fitHeight = 4,
    }
    class DocumentBookmark {
        static fromJson(obj: any): DocumentBookmark;
        copyFromJson(obj: any): boolean;
        constructor();
        private _fontStyles;
        fontStyles: DocumentFontStyles;
        private _children;
        children: Array<DocumentBookmark>;
        private _target;
        target: DocumentLinkTarget;
        private _title;
        title: string;
    }
}
declare module lt.Documents {
    class LoadDocumentOptions {
        constructor();
        resolution: number;
        name: string;
        password: string;
        annotationsUri: string;
        loadEmbeddedAnnotations: boolean;
        maximumImagePixelSize: number;
    }
}
declare module lt.Documents {
    class DocumentCharacter {
        constructor();
        private _code;
        code: number;
        codeAsString(): string;
        private _bounds;
        bounds: lt.LeadRectD;
        private _isEndOfWord;
        isEndOfWord: boolean;
        private _isEndOfLine;
        isEndOfLine: boolean;
        private _isRightToLeft;
        isRightToLeft: boolean;
    }
    class DocumentWord {
        constructor();
        private _value;
        value: string;
        private _bounds;
        bounds: lt.LeadRectD;
        private _firstCharacterIndex;
        firstCharacterIndex: number;
        private _lastCharacterIndex;
        lastCharacterIndex: number;
    }
    class DocumentPageText {
        constructor(characters: DocumentCharacter[]);
        static fromJson(obj: any): DocumentPageText;
        private _characters;
        characters: DocumentCharacter[];
        private _words;
        words: DocumentWord[];
        private _text;
        text: string;
        private _textMap;
        textMap: number[];
        clearBuildData(): void;
        buildWords(): void;
        private static reverse(s);
        private reverseCharacterPositions(index1, index2);
        buildText(): void;
        buildTextWithMap(): void;
        private doBuildText(buildMap);
        private removeSmallerDuplicate(links, link);
        parseLinks(linkPatterns: RegExp[]): DocumentLink[];
        clipText(bounds: LeadRectD): DocumentPageText;
    }
}
declare module lt.Documents {
    class ConvertItem {
        private _url;
        private _mimeType;
        private _length;
        constructor();
        static fromJson(obj: any): ConvertItem;
        private copyFromJson(obj);
        url: string;
        mimeType: string;
        length: number;
    }
    class DocumentConvertResult {
        private _archive;
        private _document;
        private _annotations;
        constructor();
        static fromJson(obj: any): DocumentConvertResult;
        private copyFromJson(obj);
        archive: ConvertItem;
        document: ConvertItem;
        annotations: ConvertItem;
    }
    class Document {
        static fromJson(obj: any): Document;
        private copyFromJson(obj);
        constructor();
        private _pages;
        private _annotations;
        private _images;
        private _barcodes;
        private _structure;
        private _values;
        private _metadata;
        private _text;
        documentId: string;
        name: string;
        uri: string;
        isDownloaded: boolean;
        isReadOnly: boolean;
        mimeType: string;
        pages: DocumentPages;
        isEncrypted: boolean;
        isDecrypted: boolean;
        isStructureSupported: boolean;
        annotations: DocumentAnnotations;
        structure: DocumentStructure;
        images: DocumentImages;
        barcodes: DocumentBarcodes;
        metadata: {
            [key: string]: string;
        };
        text: DocumentText;
        defaultResolution: number;
        static unitsPerInch: number;
        values: Internal.DocumentValues;
        annotationsUri: string;
        defaultBitsPerPixel: number;
        defaultPageSize: lt.LeadSizeD;
        documentType: string;
        password: string;
        private _serviceUserData;
        serviceUserData: any;
        validate(): any;
        private static _className;
        private static _controllerName;
        decrypt(password: string): JQueryPromise<Document>;
        convert(jobData: DocumentConverterJobData): JQueryPromise<DocumentConvertResult>;
        static documentToPixels(resolution: number, size: number): number;
        static pixelsToDocument(resolution: number, pixels: number): number;
        pointToPixels(value: lt.LeadPointD): lt.LeadPointD;
        pointToDocument(value: lt.LeadPointD): lt.LeadPointD;
        sizeToPixels(value: lt.LeadSizeD): lt.LeadSizeD;
        sizeToDocument(value: lt.LeadSizeD): lt.LeadSizeD;
        rectToPixels(value: lt.LeadRectD): lt.LeadRectD;
        rectToDocument(value: lt.LeadRectD): lt.LeadRectD;
    }
}
declare module lt.Documents.Internal {
    class ImageRequestHelper {
        static createFullUrl(baseUrl: string, paramsOb: Service.Request): string;
        static createBaseUrl(controllerName: string, actionName: string): string;
        static doGenericElementLoad(d: JQueryDeferred<Element>, imageLoader: lt.ImageLoader, baseUrl: string, params: Service.Request, className: string, methodName: string): JQueryPromise<Element>;
        static createAjaxSettings(urlMode: lt.ImageLoaderUrlMode, baseUrl: string, method: string, paramsOb: Service.Request): JQueryAjaxSettings;
        static setAjaxSettings(imageLoader: lt.ImageLoader, ajaxSettings: JQueryAjaxSettings): void;
        static setImageLoaderCallbacks(imageLoader: lt.ImageLoader, d: JQueryDeferred<Element>): void;
    }
    class PropertyBag {
        static copyMatchingProperties(objSrc: any, objTarget: any, ignoreNames: string[]): boolean;
    }
    class ArrayBufferSlicer {
        static slice(buffer: ArrayBuffer, begin: number, end: number): ArrayBuffer;
    }
}
declare module lt.Documents {
    enum DocumentUploadProgressState {
        created = 0,
        uploading = 1,
        finished = 2,
        error = 3,
        aborted = 4,
    }
    class DocumentUploadProgress {
        private _state;
        private _progress;
        private _userData;
        constructor(state: DocumentUploadProgressState, progress: number, userData: any);
        state: DocumentUploadProgressState;
        progress: number;
        userData: any;
    }
    interface AbortableJqueryPromise<T> extends JQueryPromise<T> {
        abort: () => {};
    }
    class ServiceStatus {
        private _message;
        private _time;
        private _isLicenseChecked;
        private _isLicenseExpired;
        private _isCacheAccessible;
        private _kernelType;
        constructor(message: string, time: Date, licenseChecked: boolean, licenseExpired: boolean, cacheAccessible: boolean, kernelType: string);
        message: string;
        time: Date;
        isLicenseChecked: boolean;
        isLicenseExpired: boolean;
        isCacheAccessible: boolean;
        kernelType: string;
    }
   class DocumentFactory {
        private static _className;
        private static _controllerName;
        static verifyService(): JQueryPromise<ServiceStatus>;
        static checkLicense(): JQueryPromise<boolean>;
        private static _serviceHost;
        static serviceHost: string;
        private static _servicePath;
        static servicePath: string;
        private static _serviceApiPath;
        static serviceApiPath: string;
        private static _serviceUri;
        static serviceUri: string;
        private static _serviceUserData;
        static serviceUserData: any;
        private static build();
        private static clean(value);
        private static _logErrors;
        static logErrors: boolean;
        static log(message: string): void;
        static isUploadDocumentUri(uri: string): boolean;
        static loadFromCache(documentId: string): JQueryPromise<Document>;
        static loadFromUri(uri: string, options: LoadDocumentOptions): JQueryPromise<Document>;
        static uploadFile(file: File): AbortableJqueryPromise<any>;
        static beginUpload(): JQueryPromise<string>;
        static uploadDocument(uri: string, data: ArrayBuffer, offset: number, length: number): JQueryPromise<any>;
        static abortUploadDocument(uri: string): JQueryPromise<void>;
        static downloadDocument(documentId: string, uri: string, position: number, dataSize: number): JQueryPromise<number[]>;
        static deleteFromCache(documentId: string): JQueryPromise<void>;
        private static _prepareAjax;
        static prepareAjax: PrepareAjaxEventType;
        static doPrepareAjax(sender: Object, sourceClass: string, sourceMethod: string, settings: JQueryAjaxSettings): boolean;
    }
    class PrepareAjaxEventArgs extends lt.LeadEventArgs {
        constructor(sourceClass: string, sourceMethod: string, settings: JQueryAjaxSettings);
        private _sourceClass;
        sourceClass: string;
        private _sourceMethod;
        sourceMethod: string;
        private _settings;
        settings: JQueryAjaxSettings;
        private _cancel;
        cancel: boolean;
    }
    interface PrepareAjaxEventHandler {
        (sender: any, e: PrepareAjaxEventArgs): void;
    }
    class PrepareAjaxEventType extends lt.LeadEvent {
        add(value: PrepareAjaxEventHandler): PrepareAjaxEventHandler;
        remove(value: PrepareAjaxEventHandler): void;
    }
}
interface StringStatic {
    format(...args: any[]): string;
}
declare var ltDocumentsString: StringStatic;
declare module lt.Documents {
    class ServiceError {
        private _message;
        private _detail;
        private _code;
        private _link;
        private _exceptionType;
        private _methodName;
        private _jqXHR;
        private _statusText;
        private _errorThrown;
        private _isAbortError;
        private _isParseError;
        private _isCancelError;
        private _isBrowserError;
        constructor(message: string, detail: string, code: number, link: string, exceptionType: string, methodName: string, jqXHR: JQueryXHR, statusText: string, errorThrown: string);
        message: string;
        detail: string;
        code: number;
        link: string;
        exceptionType: string;
        methodName: string;
        jqXHR: JQueryXHR;
        statusText: string;
        errorThrown: string;
        isAbortError: boolean;
        isParseError: boolean;
        isCancelError: boolean;
        isBrowserError: boolean;
        static parseError(jqXHR: JQueryXHR, statusText: string, errorThrown: string): ServiceError;
    }
}
declare module lt.Documents.Service {
    interface Request {
        userData: any;
    }
}
declare module lt.Documents.Service {
    interface Response {
        userData: any;
    }
}
declare module lt.Documents.Service {
    interface GetAnnotationsRequest extends Request {
        documentId: string;
        pageNumber: number;
        createEmpty: boolean;
    }
    interface GetAnnotationsResponse extends Response {
        annotations: string;
    }
    interface SetAnnotationsRequest extends Request {
        documentId: string;
        pageNumber: number;
        annotations: string;
    }
}
declare module lt.Documents.Service {
    interface DecryptRequest extends Request {
        documentId: string;
        password: string;
    }
    interface DecryptResponse extends Response {
        document: Document;
    }
    interface ConvertRequest extends Request {
        documentId: string;
        jobData: DocumentConverterJobData;
    }
    interface ConvertResponse extends Response {
        archive: ConvertItem;
        document: ConvertItem;
        annotations: ConvertItem;
    }
}
declare module lt.Documents.Service {
    interface LoadFromCacheRequest extends Request {
        documentId: string;
    }
    interface LoadFromCacheResponse extends Response {
        document: Document;
    }
    interface LoadFromUriRequest extends Request {
        options: LoadDocumentOptions;
        uri: string;
        resolution: number;
    }
    interface LoadFromUriResponse extends Response {
        document: Document;
    }
    interface BeginUploadResponse extends Response {
        uploadUri: string;
    }
    interface UploadDocumentRequest extends Request {
        uri: string;
        data: string;
    }
    interface DownloadRequest extends Request {
        documentId: string;
        uri: string;
        position: number;
        dataSize: number;
    }
    interface DownloadResponse extends Response {
        data: string;
    }
    interface AbortUploadDocumentRequest extends Request {
        uri: string;
    }
    interface DeleteFromCacheRequest extends Request {
        documentId: string;
    }
}
declare module lt.Documents.Service {
    interface GetThumbnailsGridRequest extends Request {
        documentId: string;
        firstPageNumber: number;
        lastPageNumber: number;
        maximumGridWidth: number;
        width: number;
        height: number;
    }
}
declare module lt.Documents.Service {
    interface GetImageRequest extends Request {
        documentId: string;
        pageNumber: number;
    }
    interface GetSvgBackImageRequest extends Request {
        documentId: string;
        pageNumber: number;
        backColor: string;
    }
    interface GetThumbnailRequest extends Request {
        documentId: string;
        pageNumber: number;
        width: number;
        height: number;
    }
    interface GetSvgRequest extends Request {
        documentId: string;
        pageNumber: number;
        options: DocumentGetSvgOptions;
        unembedImages: boolean;
    }
    interface GetTextRequest extends Request {
        documentId: string;
        pageNumber: number;
        clip: LeadRectD;
        textExtractionMode: DocumentTextExtractionMode;
    }
    interface GetTextResponse extends Response {
        pageText: DocumentPageText;
    }
    interface ReadBarcodesRequest extends Request {
        documentId: string;
        pageNumber: number;
        bounds: LeadRectD;
        maximumBarcodes: number;
        symbologies: Barcode.BarcodeSymbology[];
    }
    interface ReadBarcodesResponse extends Response {
        barcodes: Barcode.BarcodeData[];
    }
}
declare module lt.Documents.Service {
    interface ParseStructureRequest extends Request {
        documentId: string;
        parseBookmarks: boolean;
        parsePageLinks: boolean;
    }
    interface ParseStructureResponse extends Response {
        bookmarks: DocumentBookmark[];
        pageLinks: DocumentLink[][];
    }
}
declare module lt.Documents.Service {
    interface PingResponse extends Response {
        message: string;
        time: string;
        isLicenseChecked: boolean;
        isLicenseExpired: boolean;
        isCacheAccessible: boolean;
        kernelType: string;
    }
    interface CheckLicenseResponse extends Response {
        isExpired: boolean;
    }
}
