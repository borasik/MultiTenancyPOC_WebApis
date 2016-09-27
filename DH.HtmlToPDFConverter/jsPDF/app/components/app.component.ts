import { Component } from '@angular/core';

declare let jsPDF: any;

@Component({
    selector: 'my-app',
	directives: [],
    templateUrl: 'app/views/demo.view.html'
})
export class AppComponent {

    downloadFile() {
        var doc = new jsPDF();
        //doc.text(10, 10, this.content);
        //doc.save('test.pdf');

        let html = document.getElementById('terms').innerHTML;
        doc.fromHTML(html, 10, 10, {
            'width': 200
        });
        doc.save('test.pdf');
    }
}
