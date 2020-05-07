output "registry_id" {
  value       = aws_ecr_repository.booking.registry_id
  description = "Registry ID"
}

output "registry_url" {
  value       = aws_ecr_repository.booking.repository_url
  description = "Repository URL"
}

output "repository_name" {
  value       = aws_ecr_repository.booking.name
  description = "Registry name"
}