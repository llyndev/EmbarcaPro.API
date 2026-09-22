using EmbarcaPro.API.Common.Pagination;
using EmbarcaPro.API.Common.Results;
using EmbarcaPro.API.Data;
using EmbarcaPro.API.Dtos.Request;
using EmbarcaPro.API.Dtos.Response;
using EmbarcaPro.API.Models;
using EmbarcaPro.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EmbarcaPro.API.Services
{
    public class PartnerService(ApplicationDbContext context, ICurrentUser currentUser) : IPartnerService
    {

        private static readonly Expression<Func<Partner, PartnerResponse>> ToResponseExpr = p =>
            new PartnerResponse(
                p.PublicId,
                p.CnpjOrCpf,
                p.StateTaxId,
                p.LegalNameOrFullName,
                p.Address.City,
                p.Address.Uf,
                p.Address.IbgeCode,
                p.Phone,
                p.Email,
                p.IsActive);

        // Versão compilada da mesma Expression, para quando a entidade já está
        // em memória (depois de criar ou alterar)
        private static readonly Func<Partner, PartnerResponse> ToResponse = ToResponseExpr.Compile();

        public async Task<ServiceResult<PartnerResponse>> CreatePartnerAsync(CreatePartnerRequest request)
        {
            var company = await context.Companies.FirstOrDefaultAsync(c => c.Id == currentUser.CompanyId);

            if (company is null)
                return ServiceResult<PartnerResponse>.Fail("Empresa não encontrada.", ErrorType.NotFound);

            var document = Company.OnlyDigits(request.CnpjOrCpf);

            var exists = await context.Partners.AnyAsync(p => p.CnpjOrCpf == document);

            if (exists)
                return ServiceResult<PartnerResponse>.Fail("Já existe um parceiro cadastrado com este CNPJ/CPF.", ErrorType.Conflict);

            var address = new Address(
                request.Address.Street,
                request.Address.Number,
                request.Address.Complement,
                request.Address.Neighborhood,
                request.Address.City,
                request.Address.Uf,
                request.Address.State,
                Company.OnlyDigits(request.Address.IbgeCode),
                request.Address.ZipCode,
                request.Address.Phone);

            var partner = new Partner(
                company,
                cnpjOrCpf: document,
                legalNameOrFullName: request.LegalNamrOrFullName,
                address: address,
                stateTaxId: request.StateTaxId,
                phone: request.Phone,
                email: request.Email);

            context.Partners.Add(partner);

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return ServiceResult<PartnerResponse>.Fail("Já existe um parceiro cadastrado com este CNPJ/CPF.", ErrorType.Conflict);


            }

            return ServiceResult<PartnerResponse>.Ok(ToResponse(partner), "Parceiro cadastrado com sucesso.");
        }

        public async Task<ServiceResult<PagedList<PartnerResponse>>> GetAllPartnersAsync(int page, int pageSize, string? search)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 100 ? 10 : pageSize;

            var query = context.Partners.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                var digits = Company.OnlyDigits(term);

                query = query.Where(p => EF.Functions.ILike(p.LegalNameOrFullName, $"%{term}%") ||
                    (digits.Length > 0 && p.CnpjOrCpf.Contains(digits)));
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(p => p.LegalNameOrFullName)
                .ThenBy(p => p.Id)
                .Skip((page - 1 ) * pageSize)
                .Take(pageSize)
                .Select(ToResponseExpr)
                .ToListAsync();

            var paged = new PagedList<PartnerResponse>(items, totalCount, page, pageSize);

            return ServiceResult<PagedList<PartnerResponse>>.Ok(paged, "Parceiros listados com sucesso.");
        }

        public async Task<ServiceResult<PartnerResponse>> GetPartnerByPublicIdAsync(Guid id)
        {
            var response = await context.Partners
                .AsNoTracking()
                .Where(p => p.PublicId == id)
                .Select(ToResponseExpr)
                .FirstOrDefaultAsync();

            return response is null 
                ? ServiceResult<PartnerResponse>.Fail("Parceiro não encontrado.", ErrorType.NotFound)
                : ServiceResult<PartnerResponse>.Ok(response, "");

        }

        public async Task<ServiceResult<PartnerResponse>> SetPartnerActiveAsync(Guid id, bool active)
        {
            var partner = await context.Partners.FirstOrDefaultAsync(p => p.PublicId == id);

            if (partner is null)
                return ServiceResult<PartnerResponse>.Fail("Parceiro não encontrado.", ErrorType.NotFound);

            if (partner.IsActive == active)
                return ServiceResult<PartnerResponse>.Fail(active ? "O parceiro já está ativo." : "O parceiro já está inativo.", ErrorType.Conflict);

            if (active) partner.Activate();
            else partner.Deactivate();

            await context.SaveChangesAsync();

            return ServiceResult<PartnerResponse>.Ok(ToResponse(partner), active ? "Parceiro ativado." : "Parceiro desativado.");
        }
    }
}
