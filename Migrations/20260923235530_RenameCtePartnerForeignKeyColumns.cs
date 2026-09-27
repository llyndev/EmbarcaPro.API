using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EmbarcaPro.API.Migrations
{
    /// <inheritdoc />
    public partial class RenameCtePartnerForeignKeyColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "companies",
                columns: table => new
                {
                    company_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false),
                    cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    state_tax_id = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    legal_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    trade_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CrtCode = table.Column<int>(type: "integer", nullable: false),
                    rntrc = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    issuing_authority_state = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    IsProductionEnvironment = table.Column<bool>(type: "boolean", nullable: false),
                    certificate_thumbprint = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    current_series = table.Column<int>(type: "integer", nullable: false),
                    last_cte_number = table.Column<int>(type: "integer", nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    complement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    country_code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    ibge_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    state = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_companies", x => x.company_id);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    cpf = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    cnh = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    complement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    country_code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    ibge_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.driver_id);
                    table.ForeignKey(
                        name: "FK_drivers_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "partners",
                columns: table => new
                {
                    partner_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    cpnj_or_cpf = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    state_tax_id = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: true),
                    legal_name_or_full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    complement = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    country_code = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    ibge_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    neighborhood = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    state = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_partners", x => x.partner_id);
                    table.ForeignKey(
                        name: "FK_partners_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trailers",
                columns: table => new
                {
                    trailer_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    license_plate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    trailer_axle = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    max_capacity_kg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cubic_meters_volume = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trailers", x => x.trailer_id);
                    table.ForeignKey(
                        name: "FK_trailers_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "trucks",
                columns: table => new
                {
                    truck_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    license_plate = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    truck_axle = table.Column<int>(type: "integer", nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    max_capacity_kg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    is_available = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trucks", x => x.truck_id);
                    table.ForeignKey(
                        name: "FK_trucks_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    active = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    register_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "freights",
                columns: table => new
                {
                    freight_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    DriverId = table.Column<int>(type: "integer", nullable: false),
                    TruckId = table.Column<int>(type: "integer", nullable: false),
                    TrailerId = table.Column<int>(type: "integer", nullable: false),
                    OriginId = table.Column<int>(type: "integer", nullable: false),
                    DestinationId = table.Column<int>(type: "integer", nullable: false),
                    cargo_description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    estimated_weight_kg = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    freight_value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    finished_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_freights", x => x.freight_id);
                    table.ForeignKey(
                        name: "FK_freights_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_freights_drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_freights_partners_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "partners",
                        principalColumn: "partner_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_freights_partners_OriginId",
                        column: x => x.OriginId,
                        principalTable: "partners",
                        principalColumn: "partner_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_freights_trailers_TrailerId",
                        column: x => x.TrailerId,
                        principalTable: "trailers",
                        principalColumn: "trailer_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_freights_trucks_TruckId",
                        column: x => x.TruckId,
                        principalTable: "trucks",
                        principalColumn: "truck_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ctes",
                columns: table => new
                {
                    cte_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    public_id = table.Column<Guid>(type: "uuid", nullable: false),
                    uf = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    series = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    access_key = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: true),
                    issue_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    service_type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    transport_mode = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    predominant_cfop = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    origin_ibge_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    destination_ibge_code = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    company_id = table.Column<int>(type: "integer", nullable: false),
                    FreightId = table.Column<int>(type: "integer", nullable: true),
                    total_service_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    amount_receivable = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    carrier_rntrc = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    status = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    authorization_protocol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    authorization_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejection_reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    signed_xml = table.Column<string>(type: "text", nullable: true),
                    authorized_xml = table.Column<string>(type: "text", nullable: true),
                    AuthorizedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CanceledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ctes", x => x.cte_id);
                    table.ForeignKey(
                        name: "FK_ctes_companies_company_id",
                        column: x => x.company_id,
                        principalTable: "companies",
                        principalColumn: "company_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ctes_freights_FreightId",
                        column: x => x.FreightId,
                        principalTable: "freights",
                        principalColumn: "freight_id");
                });

            migrationBuilder.CreateTable(
                name: "cte_cargos",
                columns: table => new
                {
                    cargo_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cte_id = table.Column<int>(type: "integer", nullable: false),
                    cargo_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false),
                    predominant_product = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    other_characteristics = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_cargos", x => x.cargo_id);
                    table.ForeignKey(
                        name: "FK_cte_cargos_ctes_cte_id",
                        column: x => x.cte_id,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cte_events",
                columns: table => new
                {
                    cte_event_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cte_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    sequence_number = table.Column<int>(type: "integer", nullable: false),
                    event_date_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    justification = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    authorization_protocol = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    event_xml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_events", x => x.cte_event_id);
                    table.ForeignKey(
                        name: "FK_cte_events_ctes_cte_id",
                        column: x => x.cte_id,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cte_freight_components",
                columns: table => new
                {
                    freight_component_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cte_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_freight_components", x => x.freight_component_id);
                    table.ForeignKey(
                        name: "FK_cte_freight_components_ctes_cte_id",
                        column: x => x.cte_id,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cte_icms_taxes",
                columns: table => new
                {
                    icms_tax_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cte_id = table.Column<int>(type: "integer", nullable: false),
                    situation = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    tax_base = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    base_reduction_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    rate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    deferred_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    deferred_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    payable_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    withholding_tax_base = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    withholding_rate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    withholding_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    presumed_credit_percentage = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true),
                    presumed_credit_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_icms_taxes", x => x.icms_tax_id);
                    table.ForeignKey(
                        name: "FK_cte_icms_taxes_ctes_cte_id",
                        column: x => x.cte_id,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cte_partners",
                columns: table => new
                {
                    cte_partner_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CteId = table.Column<int>(type: "integer", nullable: false),
                    PartnerId = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_partners", x => x.cte_partner_id);
                    table.ForeignKey(
                        name: "FK_cte_partners_ctes_CteId",
                        column: x => x.CteId,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cte_partners_partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "partners",
                        principalColumn: "partner_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cte_referenced_invoices",
                columns: table => new
                {
                    referenced_invoice_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cte_id = table.Column<int>(type: "integer", nullable: false),
                    nfe_access_key = table.Column<string>(type: "character varying(44)", maxLength: 44, nullable: false),
                    invoice_value = table.Column<decimal>(type: "numeric(15,2)", precision: 15, scale: 2, nullable: true),
                    order_number = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_referenced_invoices", x => x.referenced_invoice_id);
                    table.ForeignKey(
                        name: "FK_cte_referenced_invoices_ctes_cte_id",
                        column: x => x.cte_id,
                        principalTable: "ctes",
                        principalColumn: "cte_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cte_cargo_quantities",
                columns: table => new
                {
                    cargo_quantity_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cargo_id = table.Column<int>(type: "integer", nullable: false),
                    unit_code = table.Column<string>(type: "text", nullable: false),
                    measure_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(15,4)", precision: 15, scale: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cte_cargo_quantities", x => x.cargo_quantity_id);
                    table.ForeignKey(
                        name: "FK_cte_cargo_quantities_cte_cargos_cargo_id",
                        column: x => x.cargo_id,
                        principalTable: "cte_cargos",
                        principalColumn: "cargo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_companies_cnpj",
                table: "companies",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cte_cargo_quantities_cargo_id",
                table: "cte_cargo_quantities",
                column: "cargo_id");

            migrationBuilder.CreateIndex(
                name: "IX_cte_cargos_cte_id",
                table: "cte_cargos",
                column: "cte_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cte_events_cte_type_seq",
                table: "cte_events",
                columns: new[] { "cte_id", "type", "sequence_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cte_freight_components_cte_id",
                table: "cte_freight_components",
                column: "cte_id");

            migrationBuilder.CreateIndex(
                name: "IX_cte_icms_taxes_cte_id",
                table: "cte_icms_taxes",
                column: "cte_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cte_partners_cte_type",
                table: "cte_partners",
                columns: new[] { "CteId", "type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cte_partners_PartnerId",
                table: "cte_partners",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "ix_cte_referenced_invoices_cte_key",
                table: "cte_referenced_invoices",
                columns: new[] { "cte_id", "nfe_access_key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_cte_referenced_invoices_key",
                table: "cte_referenced_invoices",
                column: "nfe_access_key");

            migrationBuilder.CreateIndex(
                name: "ix_ctes_access_key",
                table: "ctes",
                column: "access_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ctes_company_series_number",
                table: "ctes",
                columns: new[] { "company_id", "series", "number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ctes_FreightId",
                table: "ctes",
                column: "FreightId");

            migrationBuilder.CreateIndex(
                name: "ix_ctes_public_id",
                table: "ctes",
                column: "public_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ctes_status",
                table: "ctes",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_cnh",
                table: "drivers",
                column: "cnh",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_drivers_company_cnh",
                table: "drivers",
                columns: new[] { "company_id", "cnh" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_drivers_company_cpf",
                table: "drivers",
                columns: new[] { "company_id", "cpf" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_drivers_company_email",
                table: "drivers",
                columns: new[] { "company_id", "email" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_drivers_cpf",
                table: "drivers",
                column: "cpf",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_drivers_email",
                table: "drivers",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_freights_company_id",
                table: "freights",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "IX_freights_DestinationId",
                table: "freights",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_freights_DriverId",
                table: "freights",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_freights_OriginId",
                table: "freights",
                column: "OriginId");

            migrationBuilder.CreateIndex(
                name: "IX_freights_TrailerId",
                table: "freights",
                column: "TrailerId");

            migrationBuilder.CreateIndex(
                name: "IX_freights_TruckId",
                table: "freights",
                column: "TruckId");

            migrationBuilder.CreateIndex(
                name: "ix_partners_company_cnpjorcpf",
                table: "partners",
                columns: new[] { "company_id", "cpnj_or_cpf" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trailers_company_licenseplate",
                table: "trailers",
                columns: new[] { "company_id", "license_plate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trailers_license_plate",
                table: "trailers",
                column: "license_plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_trucks_company_licenseplate",
                table: "trucks",
                columns: new[] { "company_id", "license_plate" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trucks_license_plate",
                table: "trucks",
                column: "license_plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_CompanyId",
                table: "users",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cte_cargo_quantities");

            migrationBuilder.DropTable(
                name: "cte_events");

            migrationBuilder.DropTable(
                name: "cte_freight_components");

            migrationBuilder.DropTable(
                name: "cte_icms_taxes");

            migrationBuilder.DropTable(
                name: "cte_partners");

            migrationBuilder.DropTable(
                name: "cte_referenced_invoices");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "cte_cargos");

            migrationBuilder.DropTable(
                name: "ctes");

            migrationBuilder.DropTable(
                name: "freights");

            migrationBuilder.DropTable(
                name: "drivers");

            migrationBuilder.DropTable(
                name: "partners");

            migrationBuilder.DropTable(
                name: "trailers");

            migrationBuilder.DropTable(
                name: "trucks");

            migrationBuilder.DropTable(
                name: "companies");
        }
    }
}
