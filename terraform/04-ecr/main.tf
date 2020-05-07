provider "aws" {
  region = var.region
}

terraform {
  backend "s3" {
    bucket = "restairline-terraform-state-s3"
    key    = "dev/ecr.tfstate"
    region = "ap-east-1"
  }
}

resource "aws_ecr_repository" "booking" {
    name = var.name
    image_tag_mutability = "MUTABLE" 

    image_scanning_configuration {
        scan_on_push = false
    }

    tags = {
        name = var.name
        env = var.env
    }
}

resource "aws_ecr_lifecycle_policy" "name" {
  repository = aws_ecr_repository.booking.name

  policy = <<EOF
{
  "rules": [
    {
      "rulePriority": 1,
      "description": "Remove untagged images",
      "selection": {
        "tagStatus": "untagged",
        "countType": "imageCountMoreThan",
        "countNumber": 1
      },
      "action": {
        "type": "expire"
      }
    },
    {
      "rulePriority": 2,
      "description": "Rotate images when reach ${var.max_image_count} images stored",
      "selection": {
        "tagStatus": "any",
        "countType": "imageCountMoreThan",
        "countNumber": ${var.max_image_count}
      },
      "action": {
        "type": "expire"
      }
    }
  ]
}
EOF
}

data "aws_iam_policy_document" "resource_readonly_access" {

  statement {
    sid    = "ReadonlyAccess"
    effect = "Allow"

    principals {
      type = "*"

      identifiers = ["*"]
    }

    actions = [
      "ecr:GetAuthorizationToken",
      "ecr:BatchCheckLayerAvailability",
      "ecr:GetDownloadUrlForLayer",
      "ecr:GetRepositoryPolicy",
      "ecr:DescribeRepositories",
      "ecr:ListImages",
      "ecr:DescribeImages",
      "ecr:BatchGetImage",
      "ecr:DescribeImageScanFindings",
    ]
  }
}

data "aws_iam_policy_document" "resource_full_access" {

  statement {
    sid    = "FullAccess"
    effect = "Allow"

    principals {
      type = "*"
      identifiers = ["*"]
    }

    actions = [
      "ecr:GetAuthorizationToken",
      "ecr:InitiateLayerUpload",
      "ecr:UploadLayerPart",
      "ecr:CompleteLayerUpload",
      "ecr:PutImage",
      "ecr:BatchCheckLayerAvailability",
      "ecr:GetDownloadUrlForLayer",
      "ecr:GetRepositoryPolicy",
      "ecr:DescribeRepositories",
      "ecr:ListImages",
      "ecr:DescribeImages",
      "ecr:BatchGetImage",
      "ecr:DescribeImageScanFindings",
      "ecr:StartImageScan",
      "ecr:BatchDeleteImage",
      "ecr:SetRepositoryPolicy",
      "ecr:DeleteRepositoryPolicy",
      "ecr:DeleteRepository",
    ]
  }
}

resource "aws_ecr_repository_policy" "booking" {
  repository = aws_ecr_repository.booking.name
  policy     = data.aws_iam_policy_document.resource_full_access.json
}