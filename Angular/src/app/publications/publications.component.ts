import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { PublicationService } from "../shared/publication.service";

@Component({
  selector: "app-publications",
  templateUrl: "./publications.component.html",
  styleUrls: ["./publications.component.css"],
})
export class PublicationsComponent implements OnInit {
  categorieList;

  constructor(
    private service: PublicationService,
    private router: Router,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    this.refreshPublicationList();
  }

  refreshPublicationList() {
    this.service.getPublicationList().then((res) => (this.categorieList = res));
  }

  openForEdit(id: number) {
    this.router.navigate(["/publication/edit/" + id]);
  }
}
