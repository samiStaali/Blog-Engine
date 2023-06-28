import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { MatDialog } from "@angular/material";
import { ActivatedRoute, Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Categorie } from "src/app/shared/categorie.model";
import { CategorieService } from "src/app/shared/categorie.service";
import { PublicationService } from "src/app/shared/publication.service";
import { RetourApi } from "src/app/shared/retourApi.model";

@Component({
  selector: "app-publication",
  templateUrl: "./publication.component.html",
  styleUrls: ["./publication.component.css"],
})
export class PublicationComponent implements OnInit {
  categorieList: Categorie[];
  isValid: boolean = true;
  retour: RetourApi;
  constructor(
    private service: PublicationService,
    private categorieService: CategorieService,
    private dialog: MatDialog,
    private toastr: ToastrService,
    private router: Router,
    private currentRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    let PublicationID = this.currentRoute.snapshot.paramMap.get("id");
    if (PublicationID == null) this.resetForm();
    else {
      this.service.getPublicationByID(parseInt(PublicationID)).then((res) => {
        this.service.formData = res;
      });
    }
    this.categorieService
      .getCategorieList()
      .then((res) => (this.categorieList = res as Categorie[]));
  }

  resetForm(form?: NgForm) {
    if ((form = null)) form.resetForm();
    this.service.formData = {
      PublicationID: null,
      Titre: "",
      DatePublication: null,
      Contenu: "",
      CategorieID: null,
    };
  }

  validateForm() {
    this.isValid = true;
    if (this.service.formData.PublicationID == 0) this.isValid = false;
    return this.isValid;
  }

  onSubmit(form: NgForm) {
    if (this.validateForm()) {
      this.service.saveOrUpdatePublicationV02().then((res) => {
        this.retour = res as RetourApi;
        if (this.retour.codeRetour == "400") {
          this.toastr.error("Publication existe déja", "Blog App.");
          return;
        }
        this.resetForm();
        this.toastr.success("Submitted Successfully", "Blog App.");
        this.router.navigate(["/blog"]);
      });
    }
  }
}
